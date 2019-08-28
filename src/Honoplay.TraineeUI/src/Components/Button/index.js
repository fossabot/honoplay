import React from "react";
import "../../Styles/css/bootstrap.min.css";

export const Button = ({
  title,
  color = "success",
  onClick,
  className = null
}) => {
  return (
    <button
      onClick={() => onClick && onClick()}
      type="button"
      className={className == null ? `btn btn-${color}` : className}
    >
      {title}
    </button>
  );
};
