import { Presentation } from '../../model/presentation'

export class PresentationItem {
    public constructor(public readonly presentation: Presentation) {

    }

    public isSelected: boolean = false;
    public get id(): number | undefined { return this.presentation.id; }
    public get name(): string | undefined { return this.presentation.name; }
}