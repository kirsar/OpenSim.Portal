import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Server } from '../../model/server';
import { StorageService } from '../../service/storage-service';
import { ServerRequestBuilder } from '../../service/request-builder/server.builder'

@Component({
    selector: 'servers',
    templateUrl: './server.component.html',
    styleUrls: ['./server.component.css']
})
export class ServerComponent {
    private subscription: any;
    public server?: Server;

    constructor(
        private readonly route: ActivatedRoute,
        private readonly storage: StorageService)
    { }

    public ngOnInit() {
        this.route.params.subscribe(params => this.server = params['server']);

        this.subscription = this.route.params.subscribe(params =>
            this.storage.getServer(+params['id'], new ServerRequestBuilder()
                .withAuthor()
                .withSimulations()
                .withPresentations()).subscribe((result: Server | undefined) => this.server = result));
    }

    public ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
