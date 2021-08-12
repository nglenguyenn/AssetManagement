import React, { useEffect, useState } from "react";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import { Link, useHistory } from "react-router-dom";
import { NotificationManager } from "react-notifications";
import { USER } from "src/constants/pages";
import TextField from "src/components/FormInputs/TextField";
import DateField from "src/components/FormInputs/DateField";
import CheckboxField from "src/components/FormInputs/CheckboxField";
import SelectField from "src/components/FormInputs/SelectField";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import IUserForm from "src/interfaces/User/IUserForm";
import {
  UserTypeOptions,
  UserGenderOptions,
} from "src/constants/selectOptions";

const initialFormValues: IUserForm = {
  firstName: "",
  lastName: "",
  dateOfBirth: new Date(),
  gender: "",
  joinedDate: new Date,
  type: "",
};

const validationSchema = Yup.object().shape({
  firstName: Yup.string().trim().required("Required"),
  lastName: Yup.string().trim().required("Required"),
  dateOfBirth:Yup.string().trim().required("Required"),
  gender:Yup.string().trim().required("Required"),
  joinedDate:Yup.string().trim().required("Required"),
  type:Yup.string().trim().required("Required"),
});

type Props = {
  initialUserForm?: IUserForm;
};

const getAge = (birthDateString) => {
  const today = new Date();
  const birthDate = new Date(birthDateString);

  const yearsDifference = today.getFullYear() - birthDate.getFullYear();

  if (
    today.getMonth() < birthDate.getMonth() ||
    (today.getMonth() === birthDate.getMonth() && today.getDate() < birthDate.getDate())
  ) {
    return yearsDifference - 1;
  }
  return yearsDifference;
};

const UserFormContainer: React.FC<Props> = ({
  initialUserForm = {
    ...initialFormValues,
  },
}) => {
  const [loading, setLoading] = useState(false);
  const dispatch = useAppDispatch();
  const history = useHistory();

  return (
    <Formik
      initialValues={initialUserForm}
      enableReinitialize
      validationSchema={validationSchema}
      onSubmit={(values) => {
        setLoading(false);
      }}
    >
      {(actions) => (
        <Form className="intro-y col-lg-6 col-12">
          <TextField name="firstName" label="First Name" isrequired />
          <TextField name="lastName" label="Last Name" isrequired />
          <DateField label="Date Of Birth" name="dateOfBirth" isrequired  />
          <CheckboxField
            label="Gender"
            name="gender"
            options={UserGenderOptions}
            isrequired
          />
          <DateField label="Joined Date" name="joinedDate" isrequired />
          <SelectField
            name="type"
            label="Type"
            options={UserTypeOptions}
            isrequired
          />
          <div className="d-flex">
            <div className="ml-auto">
              <button
                className="btn btn-danger"
                type="submit"
                disabled={!(actions.isValid && actions.dirty)}
              >
                Save{" "}
                {loading && (
                  <img src="/oval.svg" className="w-4 h-4 ml-2 inline-block" />
                )}
              </button>
              <Link to={USER} className="btn btn-outline-secondary ml-2">
                Cancel
              </Link>
            </div>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default UserFormContainer;
