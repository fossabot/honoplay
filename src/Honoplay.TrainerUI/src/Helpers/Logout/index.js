import React from "react";
import WithAuth from "../../Hoc/CheckAuth";

const Logout = () => {
  localStorage.removeItem("token");
  return <></>;
};

export default WithAuth(Logout);
