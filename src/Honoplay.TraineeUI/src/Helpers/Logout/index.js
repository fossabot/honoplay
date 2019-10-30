import React from "react";
import WithAuth from "../../Hoc/CheckAuth";

const Logout = () => {
  localStorage.removeItem("token");
  localStorage.removeItem("traineeUserData");
  return <React.Fragment></React.Fragment>;
};

export default WithAuth(Logout);
