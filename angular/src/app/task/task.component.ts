import {Component, Injector, NgModule, OnInit} from '@angular/core';
import {RequestDto, TaskListDto, TaskServiceProxy} from '../../shared/service-proxies/service-proxies';
import {finalize} from 'rxjs/operators';

@Component({
    selector: 'app-task',
    templateUrl: './task.component.html',
    styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

    taskListDto: TaskListDto[];
    newTask: RequestDto;
    result: TaskListDto;

    constructor(injector: Injector,
                private taskServiceProxy: TaskServiceProxy) { }

    ngOnInit(): void {
        this.newTask = new RequestDto();
        this.newTask = new TaskListDto();
        abp.ui.setBusy();
        const vm = this;
        vm.taskServiceProxy.getAll()
            .pipe(finalize(() => { abp.ui.clearBusy(); }))
            .subscribe((result) => {
                this.taskListDto = result.items;
            });
    }

    create(): void {
        abp.ui.setBusy();
        const vm = this;
        vm.taskServiceProxy.addNewTask(this.newTask)
            .pipe(finalize(() => { abp.ui.clearBusy(); }))
            .subscribe((result) => {
            this.result = result;
            this.taskListDto = this.taskListDto.concat(this.result);
        });
    }

    delete(id: number): void {
        abp.ui.setBusy();
        const vm = this;
        vm.taskServiceProxy.deleteTask(id)
            .pipe(finalize(() => { abp.ui.clearBusy(); }))
            .subscribe((result) => {
                this.taskListDto = this.taskListDto.filter(taskListDto => taskListDto.id !== id);
            });
    }
}
