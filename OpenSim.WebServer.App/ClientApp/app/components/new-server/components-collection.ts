import { SimulationsService } from '../../service/simulations.service'
import { PresentationsService } from '../../service/presentations.service'
import { SimulationItem } from './simulation-item';
import { PresentationItem } from './presentation-item';


export class ComponentCollection {
    public constructor(
        private readonly simulationsService: SimulationsService,
        private readonly presentationsService: PresentationsService) {

        simulationsService.getAll().subscribe(
            result => this.simulations = result.map(s => new SimulationItem(s, this)),
            error => console.error(error));

        presentationsService.getAll().subscribe(
            result => this.presentations = result.map(p => new PresentationItem(p)),
            error => console.error(error));
    }

    public simulations: SimulationItem[] = [];
    public presentations: PresentationItem[] = [];

    public setDefaultSelection() {
        this.simulations.forEach(s => s.isSelected = false);
        this.presentations.forEach(s => s.isSelected = false);
    }

    public get hasSelection(): boolean {
        return this.simulations.filter(s => s.isSelected).length > 0;
    }

}