import { Observable } from 'rxjs';

export class ValueAndStream<T> {
    public constructor(public value: T, public stream: Observable<T>) { }
}