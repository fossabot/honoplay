import React from "react";
import "../../Styles/css/bootstrap.min.css";

export const Button = ({ title, color = "success", onClick }) => {
  return (
    <button
      onClick={() => onClick && onClick()}
      type="button"
      className={`btn btn-${color}`}
    >
      {title}
    </button>
  );
};
