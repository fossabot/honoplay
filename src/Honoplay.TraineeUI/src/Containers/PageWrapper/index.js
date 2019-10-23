import React, { Component } from "react";
import "../../Styles/css/all.css";
import "../../Styles/css/bootstrap.min.css";
import "../../Styles/css/global.css";

class PageWrapper extends Component {
  render() {
    const { children, className, boxNumber = "", rowClass = "" } = this.props;
    return (
      <div className={className}>
        <div className={`row ${rowClass}`}>{children}</div>
      </div>
    );
  }
}

export default PageWrapper;
