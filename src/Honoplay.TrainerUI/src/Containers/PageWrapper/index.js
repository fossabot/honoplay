import React, { Component } from "react";
import "../../Styles/css/all.css";
import "../../Styles/css/bootstrap.min.css";
import "../../Styles/css/global.css";

class PageWrapper extends Component {
  render() {
    return (
      <div className="box shadow p-5">
        <div className="row">{children}</div>
      </div>
    );
  }
}

export default PageWrapper;
