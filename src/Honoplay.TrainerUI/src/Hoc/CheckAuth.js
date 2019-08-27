import React, { Component } from "react";
import History from "../Helpers/History";

export default function WithAuth(HocComponent) {
  return class extends Component {
    componentWillMount() {
    }
    render() {
      return <HocComponent {...this.props}></HocComponent>;
    }
  };
}
