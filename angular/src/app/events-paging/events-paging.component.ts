import {Component, Injector, ViewChild} from '@angular/core';
import {PagedListingComponentBase, PagedRequestDto} from '../../shared/paged-listing-component-base';
import {
    EventListDto, EventListDtoPagedResultDto,
    EventServiceProxy,
} from '../../shared/service-proxies/service-proxies';
import {CreateEventComponent} from '../events/create-event/create-event.component';
import {finalize} from 'rxjs/operators';

class PagedEventsRequestDto extends PagedRequestDto {
    keyword: string;
    isActive: boolean | null;
}

@Component({
  selector: 'app-events-paging',
  templateUrl: './events-paging.component.html',
  styleUrls: ['./events-paging.component.css']
})
export class EventsPagingComponent extends PagedListingComponentBase<EventListDto> {
    @ViewChild('createEventModal', {static : true }) createEventModal: CreateEventComponent;

    events: EventListDto[];
    includeCanceledEvents: false;
    keyword: '';
    sorting: string | null;
    pageSize = 3; // alter the default value from 10 to 2
    // isCancelled: false;

    constructor(injector: Injector,
                private _eventService: EventServiceProxy) {
        super(injector);
    }
    includeCanceledEventsCheckboxChanged() {
        this.loadEvent();
    }
    createEvent(): void {
        this.createEventModal.display();
    }
    loadEvent() {
    }
    protected delete(entity: EventListDto): void {
    }

    protected list(request: PagedEventsRequestDto, pageNumber: number, finishedCallback: Function): void {
        request.keyword = this.keyword;

        this._eventService
            .getAll(
                request.keyword,
                this.includeCanceledEvents,
                this.sorting,
                request.skipCount,
                request.maxResultCount
            )
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: EventListDtoPagedResultDto) => {
                this.events = result.items;
                this.showPaging(result, pageNumber);
            });
    }

}
