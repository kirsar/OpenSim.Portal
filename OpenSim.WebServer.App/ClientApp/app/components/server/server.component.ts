import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Server } from '../../model/server';
import { ServersService } from '../../service/servers.service';
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
        private readonly service: ServersService)
    { }

    public ngOnInit() {
        this.subscription = this.route.params.subscribe(params =>
            this.service.get(params['id'], new ServerRequestBuilder()
                .withAuthor()
                .withSimulations()
                .withPresentations()).subscribe(
                    (result: Server | undefined) => this.server = result,
                    (error: any) => console.error(error)));
    }

    public ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
