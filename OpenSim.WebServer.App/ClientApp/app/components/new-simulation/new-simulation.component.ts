import { Component, Inject, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UploadEvent, UploadFile, FileSystemFileEntry } from 'ngx-file-drop';

@Component({
    selector: 'new-simulation-form',
    templateUrl: './new-simulation.component.html',
    styleUrls: ['./new-simulation.component.css']
})
export class NewSimulationFormComponent {
    constructor(
        private readonly http: HttpClient,
        @Inject('BASE_URL') private readonly baseUrl: string,
        private readonly changeDetection: ChangeDetectorRef) { }

    private message: string = '';
    private simulation?: Simulation; 
    private content?: any;

    @Output() public simulationCreated = new EventEmitter<Simulation>();

    public dropped(event: UploadEvent) {
        const droppedFile = event.files[0] as UploadFile;

        if (droppedFile.fileEntry.isFile) {
            const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
            fileEntry.file((file: File) => {

                // check if it's a simulation and store a file
                this.changeDetection.detectChanges();

                const reader = new FileReader();
                reader.readAsText(file);
                reader.onload = () => {
                    const json = JSON.parse(reader.result);

                    this.simulation = new Simulation(json['name']);
                    this.simulation.description = json['description'];
                    this.simulation.references = json['references'];

                    this.content = reader.result;

                    this.changeDetection.detectChanges();
                }
            });
        } else
            this.message = droppedFile.fileEntry.name + 'is not a paltform compatible simulation file';
    }

    private isValid = () => this.content !== undefined && this.content !== null;
    
    private onUpload() {
        const httpOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/hal+json' })
        };

        this.http.post(this.baseUrl + 'api/v1/simulations', this.content, httpOptions).subscribe(
            res => {
                this.simulationCreated.emit(res as Simulation);
            });

        this.content = undefined;
        this.simulation = undefined;
    }
}

class Simulation {
    constructor(
        private name: string) {
        this.references = [];
    }

    description?: string;
    references: string[];
}