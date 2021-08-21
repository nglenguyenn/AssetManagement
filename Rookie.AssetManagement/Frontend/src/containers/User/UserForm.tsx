import React, { useEffect, useState } from "react";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import { Link, useHistory } from "react-router-dom";
import { HOME, USER } from "src/constants/pages";
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
import { cleanUp, createNewUser, editUser } from "../User/reducer";
import { Status } from "src/constants/status";
import formatDate from "src/utils/formatDateTime";
import { JoinedDateOnWeekend, DateOfBirthNotEnoughAge, JoinedDateNotEnoughAge } from "src/constants/User/ErrorMessageConstants";

const initialFormValues: IUserForm = {
  firstName: "",
  lastName: "",
  gender: "",
  type: "",
  dateOfBirth: new Date(),
  joinedDate: new Date(),
};

const validationSchema = Yup.object().shape({
  firstName: Yup.string().trim().required("Required"),
  lastName: Yup.string().trim().required("Required"),
  dateOfBirth: Yup.date().nullable().required("Required").test(
    "dateOfBirth",
    DateOfBirthNotEnoughAge,
    value => value != undefined ? (calculateAge(value, Date.now()) >= 18) : false
  ),
  gender: Yup.string().required("Required"),
  joinedDate: Yup.date().nullable().required("Required")
  .test(
    "joinedDate", 
    JoinedDateOnWeekend, 
    value => value != undefined ? (value.getDay() != 0 && value.getDay() != 6): false
  )
  .test(
    "joinedDate",
    function (value) {
      const dob = this.resolve(Yup.ref("dateOfBirth"));
      if(value != undefined && dob != undefined) {
        if(calculateAge(dob as Date, (value).getTime()) < 18) {
          return this.createError({
            message: JoinedDateNotEnoughAge,
            path: "joinedDate",
          });
        } else return true;
      } else return true;
    }
  ),
  type: Yup.string().required("Required"),
});

const convertDate = (date: Date) => {
  return new Date(date.getFullYear(), date.getMonth(), date.getDate() + 1);
}

const calculateAge = (dob: Date, dateNeedCount: number) => {
  const dobData = convertDate(dob);
  const month_diff = dateNeedCount - dobData.getTime();
  const age_dt = new Date(month_diff);
  const year = age_dt.getUTCFullYear();
  return Math.abs(year - 1970);
}

type Props = {
  initialUserForm?: IUserForm;
};

const UserFormContainer: React.FC<Props> = ({
  initialUserForm = {
    ...initialFormValues,
  },
}) => {

  const { loading, status, error } = useAppSelector(
    (state) => state.userReducer
  );

  const dispatch = useAppDispatch();

  const history = useHistory();

  const isEdit = initialUserForm.id ? true : false;

  const handleCreateNewUser = (values) => {
    if (values.gender === '0') {
      values.gender = false;
    } else {
      values.gender = true;
    }
    dispatch(createNewUser(values));
  };

  const handleEditUser = (values) => {
    mapReturnGender(values);
    dispatch(editUser(values));
  };

  const mapGender = () => {
    if (initialUserForm.gender) {
      initialUserForm.gender = '1';
    } else {
      initialUserForm.gender = '0';
    }
  }

  const mapReturnGender = (values) => {
    if (values.gender === '0') {
      values.gender = false;
    } else {
      values.gender = true;
    }
  }

  useEffect(() => {
    if (status === Status.Success) {
      dispatch(cleanUp());
      history.push(USER);
    }
  }, [status, error])

  initialUserForm ={...initialUserForm, dateOfBirth: new Date(formatDate(initialUserForm.dateOfBirth ?? "")), joinedDate: new Date(formatDate(initialUserForm.joinedDate ?? ""))};

  mapGender();
  
  return (
    <Formik
      initialValues={initialUserForm}
      enableReinitialize
      validationSchema={validationSchema}
      onSubmit={(values) => {(isEdit === true) ? handleEditUser(values) :
        handleCreateNewUser(values)}
      }
    >
      {(actions) => (
        <Form className="intro-y col-lg-6 col-12">
          <TextField name="firstName" label="First Name" isrequired disabled={isEdit}/>
          <TextField name="lastName" label="Last Name" isrequired disabled={isEdit}/>
          <DateField label="Date Of Birth" name="dateOfBirth" isrequired />
          <CheckboxField
            label="Gender"
            name="gender"
            options={UserGenderOptions}
            isrequired
          />
          <DateField label="Joined Date" name="joinedDate" isrequired />
          <SelectField name="type" label="Type" options={UserTypeOptions} isrequired />
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
