import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {Dialog, Paper, DialogActions, DialogContent, 
        DialogTitle, Slide, List, FormGroup, FormControlLabel, 
        Radio , MuiThemeProvider, Grid} from '@material-ui/core';
import {Style, theme} from './Style';
import Input from '../Input/InputTextComponent';
import Button from '../Button/ButtonComponent';
import Search from '../Input/SearchInputComponent';

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
    const { data, selectedValue } = this.state;
    const {open, handleClickClose, modalTitle, modalInputName, classes} = this.props;
    return (
      <MuiThemeProvider theme={theme}>
        <div>
          <Dialog
            fullWidth
            maxWidth="md"
            open={open}
            TransitionComponent={Transition}
            onClose={handleClickClose}
            aria-labelledby="dialog-slide-title"           
          >
            <DialogTitle id="dialog-slide-title">
              {modalTitle}
            </DialogTitle>
            <DialogContent>
            <Grid container spacing={24}>
              <Grid item xs={12} sm={12}></Grid>
              <Grid item xs={10} sm={11}>
                <Input labelName={modalInputName}
                       inputType="text"                     
                />
              </Grid>
              <Grid item xs={2} sm={1}>
                <Button buttonColor="secondary" 
                        buttonName={translate('Add')}
                />
              </Grid>
              <Grid item xs={12} sm={12}></Grid>
              <Grid item xs={12} sm={9}></Grid>
              <Grid item xs={12} sm={3}>
                <Search/>
              </Grid>
              <Grid item xs={12} sm={9}></Grid>
              <Grid item xs={12} sm={12}>
                <Paper className={classes.modalPaper}>
                    {data.map((data,id) => {                   
                      return(
                        <List dense key={id} className={classes.contextDialog}> 
                          <FormGroup row>
                              <FormControlLabel
                                control= {                                  
                                  <Radio  checked={selectedValue === data.name}
                                          onClick={this.handleChange}
                                          value={data.name}
                                          color= 'secondary'
                                  />
                                }
                                label={data.name}
                              />            
                          </FormGroup>
                        </List>
                      );
                    })}    
                </Paper>
              </Grid>
              </Grid>
            </DialogContent>
            <DialogActions >
              <Button 
                buttonColor="primary" 
                buttonName= {translate('Save')}
              />
            </DialogActions>
          </Dialog>
        </div>
      </MuiThemeProvider>
    );
  }
}

export default  withStyles(Style)(ModalComponent);