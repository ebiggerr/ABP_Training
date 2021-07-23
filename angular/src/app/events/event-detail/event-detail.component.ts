import { Component, OnInit, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { EventDetailOutput, EventServiceProxy, EventListDto, EventRegisterOutput } from '@shared/service-proxies/service-proxies';

import * as _ from 'lodash';
import {finalize} from 'rxjs/operators';

@Component({
    templateUrl: './event-detail.component.html',
    animations: [appModuleAnimation()]
})

export class EventDetailComponent extends AppComponentBase implements OnInit {

    event: EventDetailOutput = new EventDetailOutput();
    eventDate: Date = new Date();
    eventId: string;

    constructor(
        injector: Injector,
        private _eventService: EventServiceProxy,
        private _router: Router,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this._activatedRoute.params.subscribe((params: Params) => {
            this.eventId = params['eventId'];
            this.loadEvent();
        });
    }

    registerToEvent(): void {
        const input = new EventListDto();
        input.id = this.event.id;
        abp.ui.setBusy();
        this._eventService.register(input)
            .pipe(finalize(() => {abp.ui.clearBusy(); }))
            .subscribe((result: EventRegisterOutput) => {
                abp.notify.success('Successfully registered to event. Your registration id: ' + result.registrationId + '.');
                this.loadEvent();
            });
    }

    cancelRegistrationFromEvent(): void {
        const input = new EventListDto();
        input.id = this.event.id;

        this._eventService.cancelRegistration(input)
            .subscribe(() => {
                abp.notify.info('Canceled your registration.');
                this.loadEvent();
            });
    }

    cancelEvent(): void {
        const input = new EventListDto();
        input.id = this.event.id;

        this._eventService.cancel(input)
            .subscribe(() => {
                abp.notify.info('Canceled the event.');
                this.backToEventsPage();
            });
    }

    isRegistered(): boolean {
        return _.some(this.event.registrations, { userId: abp.session.userId });
    }

    isEventCreator(): boolean {
        return this.event.creatorUserId === abp.session.userId;
    }

    loadEvent() {
        this._eventService.getDetailWithVenue(this.eventId)
            .subscribe((result: EventDetailOutput) => {
                this.event = result;
                this.eventDate = result.date.toDate();
            });
    }

    backToEventsPage() {
        this._router.navigate(['app/events']);
    }
}
