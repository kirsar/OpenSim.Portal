import { forkJoin } from 'rxjs';
import { Simulation } from '../../model/simulation'
import { Presentation } from '../../model/presentation'
import { ComponentCollection } from './components-collection'

export class SimulationItem {
    public constructor(
        public readonly simulation: Simulation,
        private readonly components: ComponentCollection) {
    }

    private isSelectedValue = false;
    private references: Simulation[] = [];
    private presentations: Presentation[] = [];

    public get id(): number | undefined { return this.simulation.id; }
    public get name(): string | undefined { return this.simulation.name; }

    public get isSelected(): boolean { return this.isSelectedValue; }

    public set isSelected(value: boolean) {
        this.isSelectedValue = value;

        if (!this.isSelectedValue)
            return;

        this.loadRelations();
    }

    private loadRelations() {
        forkJoin(this.simulation.queryReferences(), this.simulation.queryPresentations()).subscribe(
            ([references, presentations]) => {
                this.references = references;
                this.presentations = presentations;
                this.selectRelations();
            },
            error => console.error(error));
    }

    private selectRelations() {
        const referenceItems = this.references.map(r => this.components.simulations.find(ri => ri.id === r.id));
        referenceItems.forEach(r => r!.isSelected = true);

        const presentationItems = this.presentations.map(p => this.components.presentations.find(pi => pi.id === p.id));
        presentationItems.forEach(p => p!.isSelected = true);
    }
}