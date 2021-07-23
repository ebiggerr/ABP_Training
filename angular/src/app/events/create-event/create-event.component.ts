import {Component, ViewChild, Injector, Output, EventEmitter, ElementRef, NgModule} from '@angular/core';
import {
    EventServiceProxy,
    CreateEventWithVenueInput, VenueDto, VenueServiceProxy, EventListDtoListResultDto, VenueDtoListResultDto
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import { finalize } from 'rxjs/operators';

import * as moment from 'moment';
import {ModalDirective} from 'ngx-bootstrap';
import {NgbTimepickerConfig} from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  // styleUrls: ['./create-event.component.css']
})

export class CreateEventComponent extends AppComponentBase {

    @ViewChild('createEventModal') modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active: boolean;
    saving: boolean;
    event: CreateEventWithVenueInput;
    eventTime = {hour: 13, minute: 0};
    venues: VenueDto[];

    constructor(
        injector: Injector,
        private _eventService: EventServiceProxy,
        private _venueSerive: VenueServiceProxy,
        config: NgbTimepickerConfig
    ) {
        super(injector);
        config.spinners = false;
        this.loadVenues();
    }

    display(): void {
        // this.venues
        this.active = true;
        this.loadVenues();
        this.modal.show();
        // this.event = new CreateEventInput();
        this.event = new CreateEventWithVenueInput();
        this.event.init({ isActive: true });
    }

    save(): void {
        this.saving = true;
        console.log(this.event.venueId.toString());
        this.event.date = moment.utc(this.event.date).add(this.eventTime.hour, 'h').add(this.eventTime.minute, 'm').tz('Asia/Kuala_Lumpur');
        this._eventService.createEventWithVenue(this.event)
            .pipe(finalize(() => { this.saving = false; }))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    loadVenues() {
        this._venueSerive.getList(true)
            .subscribe((result: VenueDtoListResultDto) => {
                this.venues = result.items;
            });
    }
}
