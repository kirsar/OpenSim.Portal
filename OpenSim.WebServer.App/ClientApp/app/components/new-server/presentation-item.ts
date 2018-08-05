import { Presentation } from '../../model/presentation'

export class PresentationItem {
    public constructor(public readonly presentation: Presentation) {

    }

    public isSelected: boolean = false;
    public get name(): string | undefined { return this.presentation.name; }
}