import { AssetStates, AssetStatesForEdit } from "../constants/assetOptions";

export const getStateFromValue = (stateString: string) => {
    return AssetStates.find(item => item.value === stateString)?.label;
}

export const getStateFromId = (stateId: number) => {
    const label = AssetStatesForEdit.find(item => item.value == stateId)?.label;
    return getStateFromLabel(label ? label : "");
}

export const getStateFromLabel = (stateLabel: string) => {
    return AssetStates.find(item => item.label === stateLabel)?.value;
}