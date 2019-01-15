import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { ExpandableListModule } from 'angular2-expandable-list';
import { FileDropModule } from 'ngx-file-drop';
import { AngularHalModule } from 'angular4-hal';

import { ExternalConfigurationService } from './service/external-configuration-service';

import { ServersService } from './service/servers.service';
import { SimulationsService } from './service/simulations.service';
import { PresentationsService } from './service/presentations.service'
import { StorageService } from './service/storage-service'
//import { ErrorHandlerService } from './service/error-handler-service'
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

//const errorHandler = new ErrorHandlerService();
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
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
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
        //{ provide: ErrorHandlerService, useFactory: handler },
        //{ provide: ErrorHandler, useFactory: handler },
        { provide: 'ExternalConfigurationService', useClass: ExternalConfigurationService }
    ],
    bootstrap: [AppComponent]
})

export class AppModule {
}
