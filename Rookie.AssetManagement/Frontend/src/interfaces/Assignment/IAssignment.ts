export default interface IAssignment {
    id: number,
    assignedDate: Date,
    state: string,
    note: string,
    assignedBy: string,
    assignedTo: string,
    assetCode: string,
    assetName: string,
    specification: string,
    returnedDate: Date
}