import React, { InputHTMLAttributes, useEffect } from 'react';
import { useField } from 'formik';
import { Search } from "react-feather";

type InputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
    label: string;
    placeholder?: string;
    name: string;
    isrequired?: boolean;
    notvalidate?: boolean;
    handleClickPopup: () => void;
    selectedValue?: string
};

const TextSearchField: React.FC<InputFieldProps> = (props) => {
    const [field, { error, touched, value }, { setValue }] = useField(props);
    const { label, isrequired, notvalidate, handleClickPopup, selectedValue } = props;
    const validateClass = () => {
        if (touched && error) return 'is-invalid';
        if (notvalidate) return '';
        if (isrequired) {
            if (value != undefined && /\S/.test(value)) {
                return 'is-valid';
            }
        } else {
            return 'is-valid';
        }
        return '';
    };

    useEffect(() => {
        if(selectedValue) {
            setTimeout(() => (setValue(selectedValue)));
        }
    }, [selectedValue])

    return (
        <>
            <div className="mb-3 row">
                <label className="col-4 col-form-label d-flex">
                    {label}
                    {isrequired && (
                        <div className="invalid ml-1">(*)</div>
                    )}
                </label>
                <div className="col-lg-6">
                    <input className={`form-control ${validateClass()}`} {...field} {...props} />
                    {error && touched && (
                        <div className='invalid'>{error}</div>
                    )}
                </div>
                <div className="col">
                    <span className="border p-2 pointer">
                        <Search onClick={handleClickPopup} />
                    </span>
                </div>
            </div>

        </>
    );
};
export default TextSearchField;
