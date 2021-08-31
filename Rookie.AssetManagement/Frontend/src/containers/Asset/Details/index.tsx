import React from "react";
import { Modal } from "react-bootstrap";
import { useAppSelector } from "src/hooks/redux";
import IAsset from "../../../interfaces/Asset/IAsset";
import IAssignment from "../../../interfaces/Assignment/IAssignment";
import { convertDate } from "../../../utils/formatDateTime";
import HistoryRecord from "./HistoryRecord";
import { 
   AvailableAssetString,
   UnavailableAssetString,
   AssignedAssetString,
   WaitingForRecycleAssetString,
   RecycledAssetString,
   AvailableAssetLabel,
   UnavailableAssetLabel,
   AssignedAssetLabel,
   WaitingForRecycleAssetLabel,
   RecycledAssetLabel
} from "../../../constants/Asset/StateConstants";

type Props = {
   isShow: boolean;
   onHide?: Function;
   hide: Function;
   asset?: IAsset | null;
   historyAssignment? : Array<IAssignment> | null;
};

const AssetDetailsModal: React.FC<Props> = ({ isShow, onHide, hide, asset, historyAssignment }) => {
   const handleCancel = () => {
      hide();
   };

   const showAssetState = (state: string) => {
      const stateLabel = [
         AvailableAssetLabel,
         UnavailableAssetLabel, 
         AssignedAssetLabel, 
         WaitingForRecycleAssetLabel, 
         RecycledAssetLabel
      ];

      const stateString = [
         AvailableAssetString,
         UnavailableAssetString, 
         AssignedAssetString, 
         WaitingForRecycleAssetString, 
         RecycledAssetString
      ]
      const index = stateString.indexOf(state);
      return stateLabel[index];
   }
   return (
      <>
         <Modal centered show={isShow} onHide={onHide} dialogClassName="modal-90w modal-800w" aria-labelledby="login-modal">
            <Modal.Header closeButton>
               <Modal.Title id="login-modal">Detailed Asset Information</Modal.Title>
            </Modal.Header>

            <Modal.Body>
               <>
                  <div className="info">
                     <div className="row">
                        <div className="col-3">Asset Code</div>
                        <div className="col-8">{asset?.assetCode}</div>
                     </div>

                     <div className="row">
                        <div className="col-3">Asset Name</div>
                        <div className="col-8">{asset?.assetName}</div>
                     </div>

                     <div className="row">
                        <div className="col-3">Category</div>
                        <div className="col-8">{asset?.categoryName}</div>
                     </div>

                     <div className="row">
                        <div className="col-3">Installed Date</div>
                        <div className="col-8">{asset?.installedDate ? convertDate(asset.installedDate) : ""}</div>
                     </div>

                     <div className="row">
                        <div className="col-3">State</div>
                        <div className="col-8">{asset?.state ? showAssetState(asset.state) : ""}</div>
                     </div>

                     <div className="row">
                        <div className="col-3">Location</div>
                        <div className="col-8">{asset?.location}</div>
                     </div>

                     <div className="row">
                        <div className="col-3">Specification</div>
                        <div className="col-8">{asset?.specification}</div>
                     </div>
                     <br/>
                     <div className="row">
                        <div className="col-3">History</div>
                        <div className="col-8">
                           <HistoryRecord data={historyAssignment} />
                        </div>
                     </div>
                  </div>
               </>
            </Modal.Body>
         </Modal>
      </>
   );
};

export default AssetDetailsModal;
