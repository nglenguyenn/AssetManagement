export default interface IAssetForm {
    assetId?: number,
    assetName: string,
    category: string,
    specification: string,
    installedDate?: Date,
    assetState: string,
}