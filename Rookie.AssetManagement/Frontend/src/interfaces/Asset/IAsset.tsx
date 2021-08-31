export default interface IAsset {
    id: number,
    assetName: string,
    assetCode: string,
    specification: string;
    state: string,
    installedDate: Date,
    location: string,
    categoryId: number,
    categoryName?: string
}