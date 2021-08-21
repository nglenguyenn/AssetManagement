export default interface IAsset {
    id: number,
    assetName: string,
    assetCode: string,
    specification: string;
    assetState: string,
    installedDate: Date,
    location: string,
    categoryId: number,
}