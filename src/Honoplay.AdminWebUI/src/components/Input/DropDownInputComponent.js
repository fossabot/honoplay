import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, InputLabel, NativeSelect, IconButton } from '@material-ui/core';
import { Style, BootstrapInput, BootstrapInputError } from './Style';
import MoreVertIcon from '@material-ui/icons/MoreHoriz';
import Modal from '../Modal/ModalComponent';

class DropDownInputComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      open: false
    };
  }
  handleOpen = () => {
    this.setState({ open: true });
  };

  handleClose = () => {
    this.setState({ open: false });
  };

  render() {
    const { open } = this.state;
    const {
      classes,
      labelName,
      describable,
      onChange,
      error,
      name,
      value,
      data,
      children,
      htmlFor,
      id
    } = this.props;
    return (
      <div className={classes.inputRoot}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={2} className={classes.labelCenter}>
            <InputLabel
              htmlFor={htmlFor}
              className={classes.bootstrapFormLabel}
            >
              {labelName}
            </InputLabel>
          </Grid>
          <Grid item xs={12} sm={9}>
            <NativeSelect
              id={id}
              data={data}
              name={name}
              value={value}
              onChange={onChange}
              className={classes.nativeWidth}
              input={
                error ? (
                  <BootstrapInputError fullWidth />
                ) : (
                  <BootstrapInput fullWidth />
                )
              }
            >
              <option>{translate('Choose')}</option>
              {data &&
                data.map((data, id) => (
                  <option value={data.id} key={id}>
                    {data.name}
                  </option>
                ))}
            </NativeSelect>
          </Grid>
          {describable && (
            <Grid item xs={12} sm={1}>
              <IconButton onClick={this.handleOpen}>
                <MoreVertIcon />
              </IconButton>
            </Grid>
          )}
        </Grid>
        <Modal titleName={labelName} handleClose={this.handleClose} open={open}>
          {children}
        </Modal>
      </div>
    );
  }
}

export default withStyles(Style)(DropDownInputComponent);
