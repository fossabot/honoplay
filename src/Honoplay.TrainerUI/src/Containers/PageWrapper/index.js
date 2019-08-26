import React, { Component } from "react";
import "../../Styles/css/all.css";
import "../../Styles/css/bootstrap.min.css";
import "../../Styles/css/global.css";

class PageWrapper extends Component {
  render() {
    const { children, boxNumber = "" } = this.props;
    return (
      <div className={`box${boxNumber} shadow p-5`}>
        <div className="row">{children}</div>
      </div>
    );
  }
}

export default PageWrapper;
