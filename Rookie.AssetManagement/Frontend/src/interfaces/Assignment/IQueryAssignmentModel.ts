export default interface IQueryAssignmentModel {
    page: number;
    states: string[];
    assignedDate: Date;
    search: string;
    sortOrder: string;
    sortColumn: string;
    limit: number;
}