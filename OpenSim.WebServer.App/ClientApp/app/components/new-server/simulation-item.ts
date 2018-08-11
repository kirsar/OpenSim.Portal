import { Simulation } from '../../model/simulation'
import { Presentation } from '../../model/presentation'
import { ComponentCollection } from './components-collection'

export class SimulationItem {
    public constructor(
        public readonly simulation: Simulation,
        private readonly components: ComponentCollection) {
    }

    private _isSelected: boolean = false;
    private references?: Simulation[];
    private presentations?: Presentation[];

    public get id(): number | undefined { return this.simulation.id; }
    public get name(): string | undefined { return this.simulation.name; }

    public get isSelected(): boolean { return this._isSelected; }

    public set isSelected(value: boolean) {
        this._isSelected = value;

        if (this.references == undefined) {
            this.simulation.queryReferences().subscribe(
                references => {
                    this.references = references;
                    this.isSelected = value; // reinterable call 
                },
                error => console.error(error));
            return;
        }

        if (this.presentations == undefined) {
            this.simulation.queryPresentations().subscribe(
                presentations => {
                    this.presentations = presentations;
                    this.isSelected = value; // reinterable call 
                },
                error => console.error(error));
            return;
        }

        this.selectComponents(this._isSelected);
    }

    private selectComponents(isSelected: boolean) {
        this.references!.forEach(r => this.components.simulations.find(s => s.id === r.id)!.isSelected = isSelected); // n2 use map
        this.presentations!.forEach(r => this.components.presentations.find(s => s.id === r.id)!.isSelected = isSelected); // n2 use map
    }
}