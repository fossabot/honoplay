import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import {
  Dialog,
  Slide,
  DialogContent,
  DialogTitle
} from '@material-ui/core';
import { Style } from './Style';

function Transition(props) {
  return <Slide direction="up" {...props} />;
}

class Modal extends React.Component {

  render() {
    const { classes, open, handleClose, children } = this.props;
    return (

      <div>
        <Dialog
          fullWidth
          maxWidth='lg'
          open={open}
          onClose={handleClose}
          TransitionComponent={Transition}
        >
          <DialogContent>
            {children}
          </DialogContent>
        </Dialog>
      </div>
    );
  }
}

export default withStyles(Style)(Modal);
