import React from 'react';
import Select from 'react-select';
import { Grid, IconButton, InputLabel } from '@material-ui/core';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import Modal from '../Modal/ModalComponent';
import MoreVertIcon from '@material-ui/icons/MoreHoriz';
import { Style } from './Style';

class SelectDropdown extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedOption: null,
      open: false
    };
  }
  handleOpen = () => {
    this.setState({ open: true });
  };

  handleClose = () => {
    this.setState({ open: false });
  };

  handleChange = selectedOption => {
    this.setState({ selectedOption });
    this.props.selectedOption(selectedOption);
    //console.log(`Option selected:`, selectedOption);
  };
  render() {
    const { selectedOption, open } = this.state;
    const {
      options,
      classes,
      describable,
      labelName,
      children,
      htmlFor,
      id
    } = this.props;
    return (
      <>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={2} className={classes.labelCenterForSelect}>
            <InputLabel
              htmlFor={htmlFor}
              className={classes.bootstrapFormLabel}
            >
              {labelName}
            </InputLabel>
          </Grid>
          <Grid item xs={12} sm={9} className={classes.labelCenter}>
            <Select
              inputId={id}
              getOptionLabel={option => option.name}
              getOptionValue={option => option.id}
              value={selectedOption}
              onChange={this.handleChange}
              options={options}
              isMulti
              placeholder={translate('Choose')}
              noOptionsMessage={() => translate('NoRecordsToDisplay')}
            />
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
      </>
    );
  }
}

export default withStyles(Style)(SelectDropdown);
