import React from "react";

const Input = ({
  type = "text",
  className = "form-control mt-3",
  placeholder,
  onChange
}) => {
  return (
    <input
      type={type}
      className={className}
      placeholder={placeholder}
      onChange={e => onChange && onChange(e.target.value)}
    />
  );
};

export default Input;
