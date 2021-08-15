export default interface IAccount {
    status: string;
    id: number;
    token?: string;
    username: string;
    role: string;
    fullName: string;
    staffCode: string;
    location: string;
    isConfirmed?: boolean;
}