import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import {Dialog, Paper, DialogActions, DialogContent, 
        DialogTitle, Slide, List, ListItem, ListItemText, 
        Checkbox, MuiThemeProvider} from '@material-ui/core';
import {Style, theme} from './Style';
import Input from '../Input/InputTextComponent';
import Button from '../Button/ButtonComponent';


function Transition(props) {
  return <Slide direction="up" {...props} />;
}

class ModalComponent extends React.Component {
  constructor(props) {
      super(props);
      this.state= {
        selectedValue: '',
        data: props.data
      }
      this.handleChange=this.handleChange.bind(this);
  
  }

  handleChange (event) {
    this.setState({ selectedValue: event.target.value });
    
  };

  render() {
    const {open, handleClickClose, modalTitle, modalInputName, modalInputId, classes} = this.props;
    return (
      <MuiThemeProvider theme={theme}>
        <div>
          <Dialog
            fullWidth
            maxWidth="md"
            open={open}
            TransitionComponent={Transition}
            onClose={handleClickClose}
            aria-labelledby="alert-dialog-slide-title"
            
          >
            <DialogTitle id="alert-dialog-slide-title">
              {modalTitle}
            </DialogTitle>
            <DialogContent>
              <Paper className={classes.modalPaper}>
                {this.state.data.map(n => {
                  
                  return(
                    <List dense key={n.key} className={classes.root}>
                      <ListItem>
                        <Checkbox checked={this.state.selectedValue === n.label}
                                  onClick={this.handleChange}
                                  value={n.label}
                                  color= 'secondary'
                        />
                        <ListItemText inset primary={n.label}
                        />
                      </ListItem>
                    </List>
                  );
                })}
              </Paper>
              <DialogActions className={classes.contextDialog}>
                <Input labelName={modalInputName}
                       inputId={modalInputId} 
                       inputType="text"
                />
                <Button buttonColor="secondary" 
                        buttonName="Ekle"
                />
              </DialogActions>
            </DialogContent>
            <DialogActions >
              <Button buttonColor="primary" 
                      buttonName="Kaydet"
              />
            </DialogActions>
          </Dialog>
        </div>
      </MuiThemeProvider>
    );
  }
}

export default  withStyles(Style)(ModalComponent);