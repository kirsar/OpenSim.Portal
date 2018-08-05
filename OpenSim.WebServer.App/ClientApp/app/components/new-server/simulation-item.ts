import { Simulation } from '../../model/simulation'

export class SimulationItem {
    public constructor(public readonly simulation: Simulation) {

    }

    public isSelected: boolean = false;
    public get id(): number | undefined { return this.simulation.id; }
    public get name(): string | undefined { return this.simulation.name; }
}