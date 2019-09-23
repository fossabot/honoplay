import React from "react";
import WithAuth from "../../Hoc/CheckAuth";

const Logout = () => {
  localStorage.removeItem("token");
  return <React.Fragment></React.Fragment>;
};

export default WithAuth(Logout);
