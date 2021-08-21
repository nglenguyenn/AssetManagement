export default (date: Date | string): string => {
    return new Date(date).toLocaleString().split(',')[0];
}

export const subYear = (date: Date, year: number) => {
    date.setFullYear(date.getFullYear() - year);

    return date;
}

export const convertDate = (date) => {
    const dateData = new Date(date);
    dateData.setMinutes(dateData.getMinutes() - dateData.getTimezoneOffset());
    return [dateData.getDate(), dateData.getMonth() + 1, dateData.getFullYear()].join("/");
 };