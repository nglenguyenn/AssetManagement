export default interface IUserForm {
    id?: number,
    firstName: string,
    lastName: string,
    dateOfBirth?: Date,
    gender: string,
    joinedDate?: Date,
    type: string,
    timeOffset?: number
}