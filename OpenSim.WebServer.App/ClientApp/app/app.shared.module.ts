import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { ExpandableListModule } from 'angular2-expandable-list';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ServersComponent } from './components/servers/servers.component';
import { ServerComponent } from './components/server/server.component';
import { SimulationComponent } from './components/simulation/simulation.component';
import { PresentationComponent } from './components/presentation/presentation.component';

import { CounterComponent } from './components/counter/counter.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        ServersComponent,
        ServerComponent,
        SimulationComponent,
        PresentationComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ExpandableListModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'servers', component: ServersComponent },
            { path: 'servers/:id', component: ServerComponent },
            { path: 'simulation/:id', component: SimulationComponent },
            { path: 'presentation/:id', component: PresentationComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    bootstrap: [AppComponent]
})
export class AppModuleShared {
}
