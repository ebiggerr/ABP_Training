import {Component, ViewChild, Injector, Output, EventEmitter, ElementRef, NgModule} from '@angular/core';
import {EventServiceProxy, CreateEventInput } from '@shared/service-proxies/service-proxies';
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
    event: CreateEventInput;
    eventTime = {hour: 13, minute: 0};

    constructor(
        injector: Injector,
        private _eventService: EventServiceProxy,
        config: NgbTimepickerConfig
    ) {
        super(injector);
        config.spinners = false;
    }

    display(): void {
        this.active = true;
        this.modal.show();
        this.event = new CreateEventInput();
        this.event.init({ isActive: true });
    }

    save(): void {
        this.saving = true;
        console.log(this.event.date.toString());
        this.event.date = moment.utc(this.event.date).add(this.eventTime.hour, 'h').add(this.eventTime.minute, 'm').tz('Asia/Kuala_Lumpur');
        this._eventService.createEvent(this.event)
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
}
