import React from "react";
import { translate } from "@omegabigdata/terasu-api-proxy";
import { withStyles } from "@material-ui/core/styles";
import { Grid } from "@material-ui/core";
import Style from "../Style";
import Input from "../../components/Input/InputTextComponent";
import ImageInput from "../../components/Input/ImageInputComponent";

class BasicKnowledge extends React.Component {
  constructor(props) {
    super(props);
  }

  tenantModel = {
    name: '',
    description: '',
    logo: '',
    hostName:
      Math.random()
        .toString(36)
        .substring(2) + new Date().getTime().toString(36)
  };

  handleChange = (e) => {
    const { name, value } = e.target;
    this.tenantModel[name] = value;
    this.props.basicTenantModel(this.tenantModel);
  }

  render() {
    const { 
      classes, 
      isErrorTenant } = this.props;
      
    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <Input
              error = {isErrorTenant}
              onChange={this.handleChange}
              labelName={translate("TenantName")}
              inputType="text"
              name="name"
              value={this.tenantModel.name}
            />
            <Input
              error = {isErrorTenant}
              onChange={this.handleChange}
              labelName={translate("Description")}
              multiline
              inputType="text"
              name="description"
              value={this.tenantModel.description}
            />
            <ImageInput
              selectedImage={value => {
                this.tenantModel.logo = value;
                this.props.basicTenantModel(this.tenantModel);
              }}
              labelName={translate("TenantLogo")}
            />
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default withStyles(Style)(BasicKnowledge);
