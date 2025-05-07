import { ChangeEventHandler } from "react";

interface InputFieldProps {
  name: string;
  placeholder?: string;
  label?: string;
  type: string;
  onChange?: ChangeEventHandler<HTMLInputElement>;
  required?: boolean;
  min?: number;
  max?: number;
  disabled?: boolean;
  value?: string;
}

export default function InputField({ name, placeholder, label, type, onChange, required, min, max, disabled, value }: InputFieldProps) {
  const inputId = name ? `input-field-${name}` : undefined;

  return (
    <>
      {label && (
        <label htmlFor={inputId} className="block ml-1 mb-2 text-md text-gray-700">
          {label}
        </label>
      )}
      <input
        className={`
                    w-full min-w-64 py-4 px-3 rounded-md border-2 leading-tight
                    border-gray-500 hover:border-cyan-500 focus:border-cyan-500 text-base text-gray-900 placeholder:text-gray-500
                    focus:outline-none appearance-none
                `}
        type={type}
        name={name}
        placeholder={placeholder}
        id={inputId}
        onChange={onChange}
        required={required}
        disabled={disabled}
        min={min}
        max={max}
        value={value}
      />
    </>
  );
}
