export default interface IAccount {
    status: string;
    id: number;
    token?: string;
    userName: string;
    role: string;
    fullName: string;
    staffCode: string;
    location: string;
    isConfirmed?: boolean;
}