import React, { InputHTMLAttributes } from 'react';
import { useField } from 'formik';
import { CalendarDateFill } from "react-bootstrap-icons";
import DatePicker from 'react-datepicker';


type DateFieldProps = InputHTMLAttributes<HTMLInputElement> & {
    label: string;
    placeholder?: string;
    name: string;
    isrequired?: boolean;
    notvalidate?: boolean;
    maxDate?: Date;
    minDate?: Date;
    filterDate?: (date: Date) => boolean;
};

const DateField: React.FC<DateFieldProps> = (props) => {
    const [field, { error, touched, value }, { setValue, setTouched }] = useField(props);
    const {
        label, isrequired, notvalidate, maxDate, minDate, filterDate,
    } = props;

    const validateClass = () => {
        if (touched && error) return 'is-invalid';
        if (notvalidate) return '';
        if (isrequired) {
            if (value != null) 
                return 'is-valid';
        } else {
            if (touched) return 'is-valid';
        }
        return '';
    };

    const handleChangeAssignedDate = (assignDate: Date) => {
        setValue(assignDate);
    };

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
                    <div className="d-flex align-items-center w-100">
                        <DatePicker
                            placeholderText="MM/dd/yyyy"
                            className={`w-100 text-center form-control ${validateClass()}`}
                            dateFormat='MM/dd/yyyy'
                            selected={(field.value && new Date(field.value)) || null}
                            onChange={date => handleChangeAssignedDate(date as Date)}
                            isClearable
                            showDisabledMonthNavigation
                            showMonthDropdown
                            showYearDropdown
                            maxDate={maxDate}
                            minDate={minDate}
                            filterDate={filterDate}
                            onClickOutside={e => {setTouched(true, true)}}
                            onChangeRaw={e => {setTouched(true, true)}}
                        />

                        <div className="border p-2">
                            <CalendarDateFill />
                        </div>
                    </div>
                    {error && (
                        <div className='invalid'>{error}</div>
                    )}
                </div>
            </div>
        </>
    );
};
export default DateField;
