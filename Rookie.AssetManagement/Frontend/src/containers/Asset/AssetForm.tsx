import React, { useEffect, useState } from "react";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import { Link, useHistory } from "react-router-dom";
import TextField from "src/components/FormInputs/TextField";
import SelectField from "src/components/FormInputs/SelectField";
import TextAreaField from "src/components/FormInputs/TextAreaField";
import DateField from "src/components/FormInputs/DateField";
import IAssetForm from "src/interfaces/Asset/IAssetForm";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import { ASSET } from "src/constants/pages";
import {
  AssetCategoryOptions,
  AssetStateOptions,
  AssetStatesForEdit
} from "src/constants/assetOptions";
import CheckboxField from "src/components/FormInputs/CheckboxField";
import CheckboxStateField from "src/components/FormInputs/CheckboxStateField";
import { cleanUp, createNewAsset, editAsset } from "./reducer";
import { Status } from "src/constants/status";
import { convertDate2 as changeDate } from "src/utils/formatDateTime";

const initialFormValues: IAssetForm = {
  assetName: "",
  categoryId: 0,
  specification: "",
  state: "",
};

const validationSchema = Yup.object().shape({
  assetName: Yup.string().trim().required("Required"),
  categoryId: Yup.string().required("Required"),
  specification: Yup.string().trim().required("Required"),
  installedDate: Yup.date().nullable().required("Required"),
  state: Yup.string().required("Required"),
});

type Props = {
  initialAssetForm?: IAssetForm;
};

const AssetFormContainer: React.FC<Props> = ({
  initialAssetForm = {
    ...initialFormValues,
  },
}) => {
  const dispatch = useAppDispatch();
  const { loading, status, error } = useAppSelector(
    (state) => state.assetReducer
  );
  const isEdit = initialAssetForm.id ? true : false;
  const history = useHistory();
  const handleCreateNewAsset = (values) => {
    dispatch(createNewAsset(values));
  }

  const handleEditAsset = (values) => {
    dispatch(editAsset(values));
  };

  useEffect(() => {
    if (status === Status.Success) {
      dispatch(cleanUp());
      history.push(ASSET);
    }
  }, [status, error])

  const mapDate = () => {
    if(isEdit) {
      initialAssetForm = {...initialAssetForm, 
        installedDate: new Date(changeDate(initialAssetForm.installedDate))};
    }
  }

  mapDate();

  return (
    <Formik
      initialValues={initialAssetForm}
      enableReinitialize
      validationSchema={validationSchema}
      onSubmit={(values) => { (isEdit === false) ?
        handleCreateNewAsset(values) : handleEditAsset(values);
      }}
    >
      {(actions) => (
        <Form className="intro-y col-lg-6 col-12">
          <TextField name="assetName" label="Asset Name" />
          <SelectField
            name="categoryId"
            label="Category"
            options={AssetCategoryOptions}
            isDisabled={isEdit}
          />
          <TextAreaField name="specification" label="Specification" />
          <DateField label="Installed Date" name="installedDate" />
          <CheckboxStateField
            name="state"
            label="State"
            options={(isEdit === false ) ? AssetStateOptions : AssetStatesForEdit }
          />
          <div className="d-flex">
            <div className="ml-auto">
              <button
                className="btn btn-danger"
                type="submit"
                disabled={!(actions.isValid && actions.dirty)}
              >
                Save
                {loading && (
                  <img
                    src="/oval.svg"
                    alt=""
                    className="w-4 h-4 ml-2 inline-block mr-1"
                  />
                )}
              </button>
              <Link to={ASSET} className="btn btn-outline-secondary ml-2">
                Cancel
              </Link>
            </div>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default AssetFormContainer;
