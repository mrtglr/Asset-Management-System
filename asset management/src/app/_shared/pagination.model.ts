export class Pagination {
    page: number;
    count: number;
    pageSize: number;
    pageSizes: number[];

    constructor(page?: number, count?:number, pageSize?:number, pageSizes?: number[]){
        this.page = page;
        this.count = count;
        this.pageSize = pageSize;
        this.pageSizes = pageSizes;
    }
}
