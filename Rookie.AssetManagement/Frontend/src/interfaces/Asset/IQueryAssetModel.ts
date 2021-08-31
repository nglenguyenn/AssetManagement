export default interface IQueryAssetModel {
    page: number;
    categoryName: string[];
    state: string[];
    search: string;
    sortOrder: string;
    sortColumn: string;
    limit: number;
}