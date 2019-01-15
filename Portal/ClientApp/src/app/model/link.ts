export class Link {
    constructor(relation: string, id: number | undefined) {
        this.href = `\\${relation}\\${id}`;
    }

    public href: string;
}
