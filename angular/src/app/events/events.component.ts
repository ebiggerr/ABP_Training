import {Component, Injector, OnInit, ViewChild} from '@angular/core';
import {appModuleAnimation} from '../../shared/animations/routerTransition';
import {PagedListingComponentBase, PagedRequestDto} from '../../shared/paged-listing-component-base';
import {EventListDto, EventListDtoListResultDto, EventServiceProxy} from '../../shared/service-proxies/service-proxies';
import {CreateEventComponent} from './create-event/create-event.component';
import {AppComponentBase} from '../../shared/app-component-base';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
    animations: [appModuleAnimation()],
    styleUrls: ['./events.component.css']
})
export class EventsComponent extends PagedListingComponentBase<EventListDto> {
    @ViewChild('createEventModal', {static : true }) createEventModal: CreateEventComponent;

    active: false;
    events: EventListDto[];
    includeCanceledEvents = false;

    constructor(
        injector: Injector,
        private _eventService: EventServiceProxy,
    ) {
        super(injector);
    }

    includeCanceledEventsCheckboxChanged() {
        this.loadEvent();
    }
    createEvent(): void {
        this.createEventModal.display();
    }
    loadEvent() {
        this._eventService.getList(this.includeCanceledEvents)
            .subscribe((result: EventListDtoListResultDto) => { // changed
                this.events = result.items;
                // this.totalItems = result.items.length;
                // this.showPaging(result, this.pageNumber);
            });
    }
    protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
        this.loadEvent();
        finishedCallback();
    }

    // changed from EntityDtoOfGuid in service-proxies.ts Line 2889 with String as the only fields
    //
    protected delete(event: EventListDto): void {
    }

}
