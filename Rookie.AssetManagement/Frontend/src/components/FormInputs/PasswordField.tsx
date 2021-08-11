import React, { InputHTMLAttributes, useState } from 'react';
import { useField } from 'formik';
import { Eye, EyeFill, EyeSlashFill } from 'react-bootstrap-icons';
import { Button } from 'react-bootstrap';

type InputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
    label: string;
    placeholder?: string;
    name: string;
    isrequired?: boolean;
    notvalidate?: boolean;
};

const PasswordField: React.FC<InputFieldProps> = (props) => {
    const [visible, setVisibility] = useState(true);
    const [inputType, setInputType] = useState("password");
    const [field, { error, touched }, meta] = useField(props);
    const { label, isrequired, notvalidate } = props;


    const validateClass = () => {
        if (touched && error) return 'is-invalid';
        if (notvalidate) return '';
        if (touched) return 'is-valid';

        return '';
    };

    const handleVisible = () => {
        if (visible == true) {
            setVisibility(false);
            setInputType("password");
        }
        else {
            setVisibility(true);
            setInputType("text");
        }
    }

    return (
        <>
            <div className="mb-3 row">
                <label className="col-4 col-form-label d-flex">
                    {label}
                    {isrequired && (
                        <div className="invalid ml-1">(*)</div>
                    )}
                </label>
                <div className="col">
                    <div className="d-flex justify-content-start">
                        <input className={`form-control ${validateClass()}`} {...field} {...props} type={inputType} />
                        <button className="btn mr-3" onClick={handleVisible}>
                            {visible ? <EyeFill /> : <EyeSlashFill />}
                        </button>
                    </div>
                    {error && touched && (
                        <div className='invalid'>{error}</div>
                    )}
                </div>
            </div>

        </>
    );
};
export default PasswordField;
