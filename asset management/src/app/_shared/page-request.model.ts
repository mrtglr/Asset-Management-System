export class PageRequest<T> {
    count: number;
    pageIndex: number;
    pageSize: number;
    items: T[];
}
