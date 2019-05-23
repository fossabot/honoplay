import React from 'react';
import { CHOOSE, ADD } from '../../helpers/TerasuKey';
import { withStyles } from '@material-ui/core/styles';
import {Grid, InputLabel, NativeSelect, IconButton} from '@material-ui/core';
import {Style, BootstrapInput} from './Style';
import MoreVertIcon from '@material-ui/icons/MoreHoriz';
import Modal from '../Modal/ModalComponent';

class DropDownInputComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state={
      data: props.data,
      open: false,
    };  

    this.handleOpen=this.handleOpen.bind(this);
    this.handleClose=this.handleClose.bind(this);
  }
  handleOpen()
  {
    this.setState({ open: true });
  };

  handleClose()
  {
    this.setState({ open: false });
  };
  
  render() {

    const { classes, labelName, describable } = this.props;
    return (
        <div className={classes.inputRoot}>
        <Grid container spacing={24}>
          <Grid item xs={12} sm={3}
                className={classes.labelCenter}>
            <InputLabel  htmlFor="bootstrap-input" 
                         className={classes.bootstrapFormLabel}>
                         {labelName}
            </InputLabel>                                   
          </Grid>
          <Grid item xs={12} sm={9}>
            <NativeSelect
                className={classes.nativeWidth}
                input={<BootstrapInput fullWidth/>}
            >
                <option>{CHOOSE}</option>
                {this.state.data.map(n => 
                    <option value={n.key} key={n.key}>
                      {n.label}
                    </option>
                )}                   
            </NativeSelect>  
            {
              describable &&
              <IconButton onClick={this.handleOpen}>
                <MoreVertIcon/>
              </IconButton>  
            }        
          </Grid>
        </Grid>
        <Modal handleClickClose={this.handleClose}
               open = {this.state.open}
               data = {this.state.data}
               modalTitle = {`${labelName} ${ADD}`}
               modalInputName = {labelName}
        />
      </div>
    );
  }
}

export default withStyles(Style)(DropDownInputComponent);
