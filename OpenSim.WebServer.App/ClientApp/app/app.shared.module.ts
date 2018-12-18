import { NgModule, ErrorHandler, Injector } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { ExpandableListModule } from 'angular2-expandable-list';
import { FileDropModule } from 'ngx-file-drop';
import { AngularHalModule } from 'hal-4-angular';

import { ExternalConfigurationService } from './service/external-configuration-service';

import { ServersService } from './service/servers.service';
import { SimulationsService } from './service/simulations.service';
import { PresentationsService } from './service/presentations.service'
import { StorageService } from './service/storage-service'
import { ErrorHandlerService } from './service/error-handler-service'
import { AuthenticationService } from './service/authentication-service'
import { NavigationService } from './service/navigation-service'

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { AuthComponent } from './components/auth/auth.component'

import { ServersComponent } from './components/servers/servers.component';
import { ServerComponent } from './components/server/server.component';
import { NewServerFormComponent } from './components/new-server/new-server.component';

import { SimulationsComponent } from './components/simulations/simulations.component';
import { SimulationComponent } from './components/simulation/simulation.component';
import { NewSimulationFormComponent } from './components/new-simulation/new-simulation.component';

import { PresentationComponent } from './components/presentation/presentation.component';

const errorHandlerFactory = (injector: Injector) => errorHandler != undefined ? errorHandler : errorHandler = new ErrorHandlerService(injector);
let errorHandler: ErrorHandlerService;
//function handler() { return errorHandler; }


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        AuthComponent,
        ServersComponent,
        ServerComponent,
        NewServerFormComponent,
        SimulationsComponent,
        SimulationComponent,
        NewSimulationFormComponent,
        PresentationComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ExpandableListModule,
        FileDropModule,
        AngularHalModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'login', component: AuthComponent },
            { path: 'servers', component: ServersComponent },
            { path: 'servers/:id', component: ServerComponent },
            { path: 'simulations', component: SimulationsComponent },
            { path: 'simulations/:id', component: SimulationComponent },
            { path: 'presentations/:id', component: PresentationComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        ServersService,
        SimulationsService,
        PresentationsService,
        StorageService,
        NavigationService,
        AuthenticationService,
        { provide: 'ExternalConfigurationService', useClass: ExternalConfigurationService },
        { provide: ErrorHandlerService, useFactory: errorHandlerFactory, deps: [Injector] },
        { provide: ErrorHandler, useFactory: errorHandlerFactory, deps: [Injector] }
    ],
    bootstrap: [AppComponent]
})
export class AppModuleShared {
}
