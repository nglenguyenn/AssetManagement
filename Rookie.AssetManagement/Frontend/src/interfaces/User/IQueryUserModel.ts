export default interface IQueryUserModel {
    page: number;
    types: string[];
    search: string;
    sortOrder: string;
    sortColumn: string;
    limit: number;
}